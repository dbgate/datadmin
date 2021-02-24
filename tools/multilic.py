import sys

def format(num):
    return int(num + 0.5)

price = int(sys.argv[1])

print '1-3:', format(price) 
print '4-10:', format(price * 0.9)
print '11-20:', format(price * 0.8)
print '21-50:', format(price * 0.7)
print '51 and more:', format(price * 0.6)
