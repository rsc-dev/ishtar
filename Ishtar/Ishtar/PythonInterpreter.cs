using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System;
using System.IO;
using System.Windows.Forms;

namespace Ishtar
{
    class PythonInterpreter
    {
        private ScriptEngine engine = Python.CreateEngine();
        private ScriptScope scope = null;

        public PythonInterpreter()
        {
            this.scope = this.engine.CreateScope();
            this.scope.SetVariable("Objects", new ObjectUtils.Objects());
            this.scope.SetVariable("Heap", new ObjectUtils.Heap());
        }

        public void SetVariable(string name, object value)
        {
            this.scope.SetVariable(name, value);
        }

        public void SetTextBoxOutput(TextBox target)
        {
            this.engine.Runtime.IO.RedirectToConsole();
            Console.SetOut(TextWriter.Synchronized(new TextBoxWriter(target)));
        }

        public object Execute(string code)
        {
            ScriptSource source = this.engine.CreateScriptSourceFromString(code, SourceCodeKind.SingleStatement);
            return source.Execute(scope);
        }
    }

    class TextBoxWriter : TextWriter
    {
        private TextBox _textBox;

        public TextBoxWriter(TextBox textbox)
        {
            _textBox = textbox;
        }


        public override void Write(char value)
        {
            base.Write(value);
            // When character data is written, append it to the text box.
            _textBox.AppendText(value.ToString());
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }  
}
