
ASSEMBLY = OpenAlExamples
PACKAGES =
BINARY_LIBS = \
        Tao.OpenAl.dll \
        Tao.FreeGlut.dll \
        Tao.OpenGl.dll
SYSTEM_LIBS = \
        System \
        System.Windows.Forms \
        System.Data \
        System.Drawing
RESX = \
        OpenAlExamples.resx
RESOURCES = \
        $(ASSEMBLY).OpenAlExamples.resources
SOURCES = \
        OpenAlExamples.cs \
        Boxes.cs \
        Lesson01.cs \
        Lesson02.cs \
        Lesson03.cs \
        Lesson05.cs \
        OpenAlExamples.Designer.cs \
        Program.cs \
        Sound.cs \
        Waterfall.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) $(ASSEMBLY).csproj $(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

$(ASSEMBLY).OpenAlExamples.resources: OpenAlExamples.resx
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
	  /keyfile:$(srcdir)/../../src/Tao.OpenAl/Tao.OpenAl.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

