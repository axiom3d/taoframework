
ASSEMBLY = Tao.FreeGlut
PACKAGES =
BINARY_LIBS = \
	../Tao.OpenGl/Tao.OpenGl.dll
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Glut.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_freeglutdir = $(prefix)/lib/cli/tao-freeglut-@TAOFREEGLUT_MAJOR@
tao_freeglut_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-freeglut.pc

EXTRA_DIST = $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-freeglut.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-freeglut.pc $(DESTDIR)$(pkgconfigdir)/tao-freeglut-@TAOFREEGLUT_MAJOR@.pc
	ln -sf tao-freeglut-@TAOFREEGLUT_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-freeglut.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOFREEGLUT_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-freeglut-@TAOFREEGLUT_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-freeglut.pc

