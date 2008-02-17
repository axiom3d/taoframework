
ASSEMBLY = Tao.Platform.Windows
PACKAGES =
BINARY_LIBS = \
	../Tao.OpenGl/Tao.OpenGl.dll
SYSTEM_LIBS = \
        System \
        System.Drawing \
        System.Windows.Forms
RESOURCES =
SOURCES = \
        Gdi.cs \
        IlasmAttribute.cs \
        Kernel.cs \
        SimpleOpenGlControl.cs \
        User.cs \
        Wgl.cs \
        WglCore.cs \
        WglDelegates.cs \
        WglEnums.cs \
        WglHelper.cs \
        WinNt.cs \
        Winmm.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_platform_windowsdir = $(prefix)/lib/cli/tao-platform-windows-@TAOWINDOWS_MAJOR@
tao_platform_windows_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-platform-windows.pc

EXTRA_DIST = $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-platform-windows.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-platform-windows.pc $(DESTDIR)$(pkgconfigdir)/tao-platform-windows-@TAOWINDOWS_MAJOR@.pc
	ln -sf tao-platform-windows-@TAOWINDOWS_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-platform-windows.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOWINDOWS_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-platform-windows-@TAOWINDOWS_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-platform-windows.pc

