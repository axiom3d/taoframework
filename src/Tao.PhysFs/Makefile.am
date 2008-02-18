
ASSEMBLY = Tao.PhysFs
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Fs.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_physfsdir = $(prefix)/lib/cli/tao-physfs-@TAOPHYSFS_MAJOR@
tao_physfs_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-physfs.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj \
             $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll $(ASSEMBLY).xml
DISTCLEANFILES = tao-physfs.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-physfs.pc $(DESTDIR)$(pkgconfigdir)/tao-physfs-@TAOPHYSFS_MAJOR@.pc
	ln -sf tao-physfs-@TAOPHYSFS_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-physfs.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOPHYSFS_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-physfs-@TAOPHYSFS_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-physfs.pc

