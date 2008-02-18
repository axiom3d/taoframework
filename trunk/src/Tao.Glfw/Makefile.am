
ASSEMBLY = Tao.Glfw
PACKAGES =
BINARY_LIBS = \
	../Tao.OpenGl/Tao.OpenGl.dll
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Glfw.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_glfwdir = $(prefix)/lib/cli/tao-glfw-@TAOGLFW_MAJOR@
tao_glfw_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-glfw.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj \
             $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll $(ASSEMBLY).xml
DISTCLEANFILES = tao-glfw.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-glfw.pc $(DESTDIR)$(pkgconfigdir)/tao-glfw-@TAOGLFW_MAJOR@.pc
	ln -sf tao-glfw-@TAOGLFW_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-glfw.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOGLFW_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-glfw-@TAOGLFW_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-glfw.pc

