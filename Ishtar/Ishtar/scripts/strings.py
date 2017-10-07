from scripts import ishtar

def print_strings(is_64_bits=False):

    strings = []

    print '[+] Importing System.String'
    import clr
    clr.AddReference('System')
    from System import String
    
    clr.AddReference('ObjectUtils')
    from ObjectUtils import Objects, Heap
    
    test_str = String('DEADBEEF')
    test_str_ptr    = Objects.GetObjectAddress(test_str)
    test_str_mt     = Objects.GetMTAddress(test_str_ptr)
    test_str_type   = Objects.GetTypeByName('System.String')
    
    for heap in ishtar.MANAGED_HEAPS:
        print 'Parsing heap: {2}; {0} - {1}'.format(*heap)
        
        heap_start = heap[0]
        heap_end = heap[1]
        heap_name = heap[2]
        
        if is_64_bits:
            strings.extend(Heap.FindObjectsByMTAddress64(heap_start, heap_end, test_str_mt, test_str_type))
        else:
            strings.extend(Heap.FindObjectsByMTAddress32(heap_start, heap_end, test_str_mt, test_str_type))
        
        
    print '[+] Done.'
    return strings
# end-of-function print_strings


if __name__ == '__main__':
    pass