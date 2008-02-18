
ASSEMBLY = Tao.Sdl.Tests
PACKAGES = \
	nunit
BINARY_LIBS = \
	../../src/Tao.Sdl/Tao.Sdl.dll
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        SdlTest.cs \
        SdlTestGfx.cs \
        SdlTestImage.cs \
        SdlTestMixer.cs \
        SdlTestTtf.cs \
        SdlTestVideo.cs \
        SmpegTest.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_sdl_testsdir = $(prefix)/lib/cli/tao-sdl-tests-@TAOSDL_MAJOR@

noinst_DATA = $(ASSEMBLY).dll tao-sdl-tests.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-sdl-tests.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS)
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /pkg:, $(PACKAGES)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES))

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-sdl-tests.pc $(DESTDIR)$(pkgconfigdir)/tao-sdl-tests-@TAOSDL_MAJOR@.pc
	ln -sf tao-sdl-tests-@TAOSDL_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-sdl-tests.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOSDL_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-sdl-tests-@TAOSDL_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-sdl-tests.pc

