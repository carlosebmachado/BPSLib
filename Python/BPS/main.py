import bps

path = 'D:/Documentos/OneDrive/DESKTOP/BPSLib/'
wf = 'write_test'
rf = 'read_test.bps'

bpsFile = bps.read(path + rf)

for s in bpsFile.findAll():
    print(s.Name)
    for d in s.findAll():
        print(d.Key + ":" + d.Value)
    print("\n")

bps.write(bpsFile, path + wf)
