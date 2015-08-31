import msvcrt
import threading
import _thread
import sys
import time

stop = False

def waitkeyboard():
    key = msvcrt.getch()
    if key == b'\x1b':
        global stop
        stop = True

_thread.start_new_thread(waitkeyboard, ())

while not stop:
    print('test')
    time.sleep(2)