
ASSEMBLY = GlfwExamples
PACKAGES =
BINARY_LIBS = \
        Tao.Glfw.dll \
        Tao.OpenGl.dll
SYSTEM_LIBS = \
        System \
        System.Windows.Forms \
        System.Data \
        System.Drawing
RESX = \
        GlfwExamples.resx
RESOURCES = \
        $(ASSEMBLY).GlfwExamples.resources
SOURCES = \
        Boing.cs \
        Cube.cs \
        Gears.cs \
        GlfwExamples.cs \
        GlfwExamples.Designer.cs \
        GlslLesson01.cs \
        KeyTest.cs \
        ListModes.cs \
        Pong.cs \
        Program.cs \
        Triangle.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) $(ASSEMBLY).csproj $(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

$(ASSEMBLY).GlfwExamples.resources: GlfwExamples.resx
	$(RESGEN) $< $@

$(ASSEMBLY).exe: $(RESOURCES) $(SOURCES)
	for lib in $(BINARY_LIBS); do \
	  ln -sf ../../src/$${lib%.dll}/$${lib}; \
	  ln -sf ../../src/$${lib%.dll}/$${lib}.config; \
	done
	$(CSC) /out:$@ /target:winexe \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /resource:, $(RESOURCES)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /keyfile:$(srcdir)/../../src/Tao.Glfw/Tao.Glfw.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

