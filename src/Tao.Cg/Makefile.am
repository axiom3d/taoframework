
ASSEMBLY = Tao.Cg
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Cg.cs \
        CgGl.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_cgdir = $(prefix)/lib/cli/tao-cg-@TAOCG_MAJOR@
tao_cg_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-cg.pc

EXTRA_DIST = $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-cg.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-cg.pc $(DESTDIR)$(pkgconfigdir)/tao-cg-@TAOCG_MAJOR@.pc
	ln -sf tao-cg-@TAOCG_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-cg.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOCG_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-cg-@TAOCG_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-cg.pc

