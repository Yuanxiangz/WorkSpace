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

servieList=['Eze.Ims.BootstrapBuilder', 'Eze.Ims.Topology', 'Eze.DataService']
for i in range(1, 23):
    servieList.append("EzeImsService_"+str(i))

for service in servieList:
    stopservice(service)

for service in servieList:
    startservice(service)

print("Restart complete!")