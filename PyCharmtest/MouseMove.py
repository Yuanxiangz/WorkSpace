__author__ = 'allenzhu'

import win32gui
from ctypes import *
import time
import msvcrt
import _thread

stop=False
def waitkeyboard():
    key=msvcrt.getch()
    if key==b'\x1b':
        global stop
        stop=True

_thread.start_new_thread(waitkeyboard, ())

distance=50
while not stop:
    x,y=win32gui.GetCursorPos()
    x+=distance
    y+=distance
    windll.user32.SetCursorPos(x, y)
    time.sleep(2)
    distance=-distance
