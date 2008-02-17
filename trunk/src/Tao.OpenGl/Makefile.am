
ASSEMBLY = Tao.OpenGl
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        GLCore.cs \
        GLDelegates.cs \
        GLEnums.cs \
        Gl.cs \
        GlExtensionLoader.cs \
        GlHelper.cs \
        Glu.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_opengldir = $(prefix)/lib/cli/tao-opengl-@TAOOPENGL_MAJOR@
tao_opengl_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-opengl.pc

EXTRA_DIST = $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-opengl.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /keyfile:$(srcdir)/$(ASSEMBLY).snk
#/doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-opengl.pc $(DESTDIR)$(pkgconfigdir)/tao-opengl-@TAOOPENGL_MAJOR@.pc
	ln -sf tao-opengl-@TAOOPENGL_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-opengl.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOOPENGL_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-opengl-@TAOOPENGL_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-opengl.pc

