version=$(date +2.0.0.svn%Y%m%d)
pkgdir=taoframework-$version
rm -Rf $pkgdir
mkdir $pkgdir

for subdir in OpenGl DevIl FFmpeg Lua Ode PhysFs Sdl; do
   (cd src/Tao.$subdir
    rm -Rf autotools
    mono ../../other/Prebuild/Prebuild.exe /target autotools
    cd autotools/Tao.$subdir
    sed -i 's/--add-missing/& --copy/' autogen.sh */autogen.sh
    sed -i 's/\(EXTRA_DIST=\)\(install-sh.*\)/\1"\2"/' configure.ac
    NOCONFIGURE=1 /bin/sh autogen.sh
    cd ../../../../
    mv src/Tao.$subdir/autotools/Tao.$subdir $pkgdir/
    rm -Rf src/Tao.$subdir/autotools)
done

tar czf taoframework_$version.orig.tar.gz $pkgdir
rm -Rf $pkgdir

