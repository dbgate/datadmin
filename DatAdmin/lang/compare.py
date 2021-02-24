import sys

def load_lang_file(fn):
    dct = {}
    for l in open(fn):
        try:
            key, value = l.split('=', 1)
            dct[key] = value
        except:
            continue
    return dct

en = load_lang_file('base.en')
other = load_lang_file('base.' + sys.argv[1])

missing = set()
redundant = set()

for s in en:
    if s not in other:
        missing.add(s)

for s in other:
    if s not in en:
        redundant.add(s)

print '************************* REDUNTANT:'
for s in redundant:
    print s

print '************************* MISSING:'

for u in missing:
    print u + '=' + en[u].strip()
