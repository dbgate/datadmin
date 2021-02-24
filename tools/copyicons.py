import sys, os, os.path, shutil

copydirs = []

for d in os.listdir('..'):
    if d == 'DatAdmin' or d.startswith('Plugin.'):
        fd = '../%s/Resources' % d
        if os.path.isdir(fd):
            copydirs.append(fd)
            print 'Detected output directory:', d


    
for fn in os.listdir('../DatAdmin.Core/Resources'):
    for dstd in copydirs:
        dstf = os.path.join(dstd, fn)
        if os.path.isfile(dstf):
            print 'Copying:', dstf
            shutil.copyfile(os.path.join('../DatAdmin.Core/Resources', fn), dstf)
