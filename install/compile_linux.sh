cd .dinst
gzip -d datadmin-debian.tgz
tar xvf datadmin-debian.tar
find . -type d -exec chmod 755 {} \;
find . -type f -exec chmod 644 {} \;
chmod +x ./datadmin-debian/usr/bin/datadmin
chmod +x ./datadmin-debian/usr/bin/daci
chmod +x ./datadmin-debian/DEBIAN/postinst
chmod +x ./datadmin-debian/DEBIAN/prerm
dpkg -b datadmin-debian datadmin-debian.deb
