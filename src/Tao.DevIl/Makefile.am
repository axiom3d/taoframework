
ASSEMBLY = Tao.DevIl
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Il.cs \
        Ilu.cs \
        Ilut.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_devildir = $(prefix)/lib/cli/tao-devil-@TAODEVIL_MAJOR@
tao_devil_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-devil.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj \
             $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll $(ASSEMBLY).xml
DISTCLEANFILES = tao-devil.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-devil.pc $(DESTDIR)$(pkgconfigdir)/tao-devil-@TAODEVIL_MAJOR@.pc
	ln -sf tao-devil-@TAODEVIL_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-devil.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAODEVIL_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-devil-@TAODEVIL_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-devil.pc

