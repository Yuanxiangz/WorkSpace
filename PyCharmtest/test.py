__author__ = 'allenzhu'

import msvcrt

key=msvcrt.getch()
if key=='\\x1b':
    print('Escape')

print('test')