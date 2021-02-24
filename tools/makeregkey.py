import md5, sys, random, datetime

def shash(data):
    return md5.md5(data).digest().encode('base64')[:-3]

email = sys.argv[1]

def extract_penta(b, i):
    if i == 0: return b & 31
    if i == 1: return (b & 992) >> 5
    if i == 2: return (b & 31744) >> 10
    raise Exception('Byte out of range' + str(i))

BASE32 = 'VB2UYCDH38WMKERS57Z6XN4G9QTJLPAF';
    
def repair_triple(a, b, c, check):
    if a == -1:
        a = 0
        while (a + b + c) % 32 != check: a += 1
    if b == -1:
        b = 0
        while (a + b + c) % 32 != check: b += 1
    if c == -1:
        c = 0
        while (a + b + c) % 32 != check: c += 1
    return (a, b, c)
    #return BASE32[a] + BASE32[b] + BASE32[c]    

def readbit(n):
    global binkey
    index = n / 5
    mask = 1 << (n % 5)
    return (binkey[index] & mask) != 0

def writebit(n, value):
    global binkey
    index = n / 5
    mask = 1 << (n % 5)
    if value: binkey[index] = (binkey[index] | mask) % 32
    else: binkey[index] = (binkey[index] & (~mask)) % 32
    

def change(n1, n2):
    b1 = readbit(n1)
    b2 = readbit(n2)
    writebit(n1, b2)
    writebit(n2, b1)

def encode_triple(index):
    global binkey
    return BASE32[binkey[index]] + BASE32[binkey[index + 1]] + BASE32[binkey[index + 2]]

p2 = shash("ge3egsxg" + email + "e3fg093etg3ge")
p2_eval = shash("dgsn984" + email + "e3fg093etg3ge")
p3_prof = shash(email + "6fr3gfg3pojf3" + p2 + "fdf" + p2 + email)
p3_ult = shash(email + "h9g4rnvj54f" + p2 + "ju3" + p2 + email)
p3_eval = shash(email + "6fr3gfg3pojf3" + p2_eval + "fdf" + p2_eval + email)
key_prof = p3_prof[:12].upper().replace('+', 'N').replace('/', '7')
key_ult = p3_ult[:12].upper().replace('+', 'N').replace('/', '7')
key_eval = p3_eval[:12].upper().replace('+', 'N').replace('/', '7')
print 'Professional:', key_prof[0:3] + '-' + key_prof[3:6] + '-' + key_prof[6:9] + '-' + key_prof[9:12]
print 'Ultimate:', key_ult[0:3] + '-' + key_ult[3:6] + '-' + key_ult[6:9] + '-' + key_ult[9:12]
print 'Evaluation:', key_eval[0:3] + '-' + key_eval[3:6] + '-' + key_eval[6:9] + '-' + key_eval[9:12]

mailsum = 0
for ch in email: mailsum += ord(ch)
days = (datetime.datetime.now() - datetime.datetime(2009, 1, 1)).days + 31; # evaluation period
d1 = extract_penta(days, 0)
d2 = extract_penta(days, 1)
d3 = extract_penta(days, 2)
dcheck = ((d1+1) * (d2+2) * (d3+3)) % 32

binkey = []
binkey.extend(repair_triple(-1, extract_penta(mailsum, 0), random.randrange(0, 32), 13))
binkey.extend(repair_triple(d2, -1, dcheck, 27))
binkey.extend(repair_triple(-1, d1, random.randrange(0, 32), 22))
binkey.extend(repair_triple(d3, -1, extract_penta(mailsum, 1), 9))

# changing is not used now
# change(44, 43);
# change(34, 23);
# change(59, 26);
# change(16, 30);
# change(24, 47);
# change(52, 38);
# change(13, 45);
# change(21, 55);
# change(28, 47);
# change(37, 57);
# change(51, 59);
# change(48, 53);
# change(4, 50);
# change(42, 46);
# change(15, 13);
# change(0, 17);
# change(34, 35);
# change(23, 1);
# change(43, 1);
# change(13, 34);
# change(6, 7);
# change(15, 25);
# change(51, 11);
# change(54, 36);
# change(19, 21);
# change(37, 50);
# change(51, 39);
# change(55, 13);
# change(54, 31);
# change(10, 11);
# change(11, 59);
# change(12, 21);
# change(6, 34);
# change(46, 17);
# change(43, 15);
# change(52, 16);
# change(1, 15);
# change(56, 38);
# change(7, 42);
# change(24, 12);
# change(52, 14);
# change(30, 57);
# change(4, 33);
# change(8, 58);
# change(17, 13);
# change(39, 9);
# change(49, 50);
# change(39, 13);
# change(59, 43);
# change(52, 15);
# change(12, 9);
# change(32, 41);
# change(10, 18);
# change(1, 4);
# change(34, 58);
# change(47, 33);
# change(39, 2);
# change(39, 15);
# change(18, 6);
# change(51, 11);

#print d1, d2, d3, dcheck
eval_new = ''
eval_new += encode_triple(0)
eval_new += '-' 
eval_new += encode_triple(3)
eval_new += '-' 
eval_new += encode_triple(6)
eval_new += '-' 
eval_new += encode_triple(9)
print 'Eval - new:', eval_new 

