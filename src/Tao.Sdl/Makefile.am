
ASSEMBLY = Tao.Sdl
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Sdl.cs \
        SdlGfx.cs \
        SdlImage.cs \
        SdlMixer.cs \
        SdlNet.cs \
        SdlTtf.cs \
        Smpeg.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_sdldir = $(prefix)/lib/cli/tao-sdl-@TAOSDL_MAJOR@
tao_sdl_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-sdl.pc

EXTRA_DIST = $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-sdl.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-sdl.pc $(DESTDIR)$(pkgconfigdir)/tao-sdl-@TAOSDL_MAJOR@.pc
	ln -sf tao-sdl-@TAOSDL_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-sdl.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOSDL_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-sdl-@TAOSDL_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-sdl.pc

