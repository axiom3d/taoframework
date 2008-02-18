
ASSEMBLY = Tao.FreeType
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        FreeType.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_freetypedir = $(prefix)/lib/cli/tao-freetype-@TAOFREETYPE_MAJOR@
tao_freetype_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-freetype.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj \
             $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll $(ASSEMBLY).xml
DISTCLEANFILES = tao-freetype.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-freetype.pc $(DESTDIR)$(pkgconfigdir)/tao-freetype-@TAOFREETYPE_MAJOR@.pc
	ln -sf tao-freetype-@TAOFREETYPE_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-freetype.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOFREETYPE_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-freetype-@TAOFREETYPE_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-freetype.pc

