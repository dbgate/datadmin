import sys

notused = set()

for line in open('notused.txt'):
    notused.add(line.strip())

for line in open(sys.argv[1]):
    line = line.strip()
    ar = line.split('=')
    if len(ar) > 0 and ar[0] in notused:
        line = ';' + line
    print line
