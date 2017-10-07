__author__      = 'Radoslaw Matusiak'
__copyright__   = 'Copyright (c) 2017 Radoslaw Matusiak'
__license__     = 'MIT'
__version__     = '0.1'

import clr
import imp
import os
import tempfile

MANAGED_HEAPS = []  # List of tupples (start_address, stop_address, details) describing managed heaps


def test_objects():
    import clr
    clr.AddReference('System')
    import System
    
    t = System.String('test')
    t_ptr = Objects.GetObjectAddress(t)
    
    x = Objects.GetInstance(t_ptr, Objects.CLR_VERSION.VER_4_0)
# end-of-function test


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


if __name__ == '__main__':
    pass