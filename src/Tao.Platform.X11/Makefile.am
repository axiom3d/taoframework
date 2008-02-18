
ASSEMBLY = Tao.Platform.X11
PACKAGES =
BINARY_LIBS = \
	../Tao.OpenGl/Tao.OpenGl.dll
SYSTEM_LIBS = \
        System \
        System.Drawing \
        System.Windows.Forms
RESOURCES =
SOURCES = \
        API.cs \
        Functions.cs \
        Glx.cs \
        Structs.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_platform_x11dir = $(prefix)/lib/cli/tao-platform-x11-@TAOX11_MAJOR@
tao_platform_x11_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-platform-x11.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj \
             $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll $(ASSEMBLY).xml
DISTCLEANFILES = tao-platform-x11.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-platform-x11.pc $(DESTDIR)$(pkgconfigdir)/tao-platform-x11-@TAOX11_MAJOR@.pc
	ln -sf tao-platform-x11-@TAOX11_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-platform-x11.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOX11_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-platform-x11-@TAOX11_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-platform-x11.pc

