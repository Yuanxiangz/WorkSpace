__author__ = 'allenzhu'

import os

def stopservice(name):
    result = os.popen('sc query %s' % name).read()
    if 'RUNNING' in result:
        print('The Service %s is running........' % name)
        os.popen('sc stop %s' % name).read()
        print('Stop Service ........')
    elif 'START_PENDING' in result:
        print('The Service  %s is starting........' % name)
        os.popen('sc stop %s' % name).read()
        print('Stop Service ........')
    elif 'STOP_PENDING' in result:
        print('The Service  %s is stopping........' % name)
    elif 'STOPPED' in result:
        print('The Service  %s stopped........' % name)
    else:
        print('The Service %s is in other status........' % name)

def startservice(name):
    print('The Service %s is starting....... ' % name)
    os.popen('sc start %s' % name).read()

serviceList=['Eze.Ims.BootstrapBuilder', 'Eze.Ims.Topology', 'Eze.DataService']
for i in range(1, 23):
    serviceList.append('EzeImsService_%s' % i)

for service in serviceList:
    stopservice(service)

#for service in serviceList:
#    startservice(service)

print("Restart complete!")