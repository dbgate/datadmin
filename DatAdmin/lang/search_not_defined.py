import os, os.path, re, sys

defined = set()
undefined = set()
used = set()
notused = set()
defs = {}

for l in open(sys.argv[1]):
    try:
        key, value = l.split('=', 1)
        defined.add(key)
    except:
        continue

try:
    for l in open(sys.argv[2]):
        key, value = l.split('=', 1)
        defs[key] = value
except:
    pass

for root, dirs, files in os.walk('../..'):
    if '.svn' in dirs:
        dirs.remove('.svn')
    if '.bld' in dirs:
        dirs.remove('.bld')
    if 'Plugin.apps' in dirs:
        dirs.remove('Plugin.apps')

    for name in files:
        fn = os.path.join(root, name)
        if fn.endswith('.cs') or fn.endswith('adx') or fn.endswith('adl'):
            print >>sys.stderr, 'Processing file:', fn
            data = open(fn).read()
            pat = re.compile(r'[\s\'"\/\>{](s_[0-9a-z_\$]*)')
            for m in pat.finditer(data):
                s = m.group(1)
                used.add(s)

for s in used:
    if s not in defined:
        undefined.add(s)

for s in defined:
    if s not in used:
        notused.add(s)

print '************************* NOT USED:'
for s in notused:
    print s

print '************************* NOT DEFINED:'

for u in undefined:
    if u in defs: print u + '=' + defs[u].strip()
    else: print u + '='
