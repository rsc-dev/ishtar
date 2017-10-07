__author__      = 'Radoslaw Matusiak'
__copyright__   = 'Copyright (c) 2017 Radoslaw Matusiak'
__license__     = 'MIT'
__version__     = '0.1'

import clr
import imp
import os
import sys
import tempfile

MANAGED_HEAPS = []  # List of tupples (start_address, stop_address, details) describing managed heaps


def on_load():
    """
    Execute when module is imported.    
    """
    print '[+] Loading Ishtar script'
    print '[+] PID: {0}'.format(os.getpid())
    print '[+] Current workin directory: {0}'.format(os.getcwd())
# end-of-function on_load
on_load()


def test_objects():
    import clr
    clr.AddReference('System')
    import System
    
    t = System.String('test')
    t_ptr = Objects.GetObjectAddress(t)
    
    x = Objects.GetInstance(t_ptr, Objects.CLR_VERSION.VER_4_0)
# end-of-function test


def run_vmmap(vmmap):
    """
    Run vmmap and fill MANAGED_HEAPS variable.
    
    Arguments:
    vmmap -- Full path to vmmap.exe.
    
    Returns: None.
    """
    if os.path.isfile(vmmap):
        print '[+] Running vmmap...'
        pid = os.getpid()
        vmmap_csv = os.path.join(os.getcwd(), 'vmmap.csv')
        os.system('{0} -p {1} {2}'.format(vmmap, pid, vmmap_csv))
        
        print '[+] Parsing vmmap output...'
        parse_vmmap_file(vmmap_csv)
        
        print '[+] Cleaning...'
        os.remove(vmmap_csv)
        print '[+] Done!'
    else:
        print '[!] {} not a file.'.format(vmmap)
# end-of-function run_vmmap


def parse_vmmap_file(vmmap_csv):
    """
    Use vmmap report to find all managed heaps and fill global MANAGED_HEAPS list.
    
    Arguments:
    vmmap_csv -- Full path to vmmap output CSV file.
    
    Returns: None.
    """
    global MANAGED_HEAPS

    with open(vmmap_csv, 'r') as fh:
        for l in fh.readlines():
            # Look for *NOT RESERVED*, *MANAGED HEAP* fragments
            if 'Managed Heap' in l and l.startswith('"  ') and 'Reserved' not in l and len(l) > 0:
                # Clean up line
                l = l.replace('\xa0', '')
                l = l.replace('"', '').strip()
                tokens = l.split(',')
                
                # Find relevant tokens
                start = int(tokens[0].replace('"', ''), 16)
                size = tokens[2].replace('"', '')
                
                if 'bytes' in size:
                    size = int(size.replace(' bytes', ''))  # vmmap reports are soo human redable
                else:
                    size = int(size) * 1024  # Size in KB
                
                
                # Update list with tupple (start_address, stop_address, details)
                MANAGED_HEAPS.append( (start, start + size, tokens[-1]) )
# end-of-function managed_heaps


def clr_import(module_name):
    """Add reference to managed module and import it.
    
    Arguments:
    module_name -- Managed module name.
    
    Returns: None.
    """
    clr.AddReference(module_name)
    imp.load_module(module_name, *imp.find_module(module_name))
# end-of-function clr_import


if __name__ == '__main__':
    pass