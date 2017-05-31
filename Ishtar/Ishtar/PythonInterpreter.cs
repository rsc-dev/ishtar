using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Windows.Forms;

namespace Ishtar
{
    /// <summary>
    /// IronPython embedded interpreter class.
    /// </summary>
    class PythonInterpreter
    {
        private ScriptEngine engine = Python.CreateEngine();
        private ScriptScope scope = null;

        /// <summary>
        /// Ctor.
        /// </summary>
        public PythonInterpreter()
        {
            this.scope = this.engine.CreateScope();
            this.scope.SetVariable("Objects", new ObjectUtils.Objects());
            this.scope.SetVariable("Heap", new ObjectUtils.Heap());
        }

        /// <summary>
        /// Redirect current engine output to goven TextBox.
        /// </summary>
        /// <param name="target">Target TextBox.</param>
        public void SetTextBoxOutput(TextBox target)
        {
            this.engine.Runtime.IO.RedirectToConsole();
            Console.SetOut(TextWriter.Synchronized(new TextBoxWriter(target)));
        }

        /// <summary>
        /// Execute Python code in current engine scope.
        /// </summary>
        /// <param name="code">Python code.</param>
        /// <returns>Returns an object that is resulting value of running the code.</returns>
        public object Execute(string code)
        {
            ScriptSource source = this.engine.CreateScriptSourceFromString(code, SourceCodeKind.SingleStatement);
            return source.Execute(scope);
        }
    }

    /// <summary>
    /// Helper class for TextBox output redirection.
    /// </summary>
    class TextBoxWriter : TextWriter
    {
        private TextBox _textBox;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="textbox"></param>
        public TextBoxWriter(TextBox textbox)
        {
            this._textBox = textbox;
        }

        /// <summary>
        /// Overriden Write.
        /// </summary>
        /// <param name="value"></param>
        public override void Write(char value)
        {
            base.Write(value);
            // When character data is written, append it to the text box.
            _textBox.AppendText(value.ToString());
        }

        /// <summary>
        /// Overriden encoding property.
        /// </summary>
        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }  
}
