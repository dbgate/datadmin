import os, os.path, sys, re

incregex = re.compile(r'#\[include ([a-z\.]+)(\,([^\]]+))?\]') 

def repl(m):
    s = open(m.group(1)).read()
    if m.group(3) is not None:
        s = s.replace('#RULENAME#', 'name="%s"' % m.group(3))
    else:
        s = s.replace('#RULENAME#', '')
    return s

for fn in os.listdir('.'):
    if not fn.lower().endswith('.xshd'): continue
    print 'Processing', fn
    s = open(fn).read()
    s = incregex.sub(repl, s)
    open('../' + fn, 'w').write(s)
