
ASSEMBLY = NeHe
PACKAGES =
BINARY_LIBS = \
        Tao.FreeGlut.dll \
        Tao.OpenGl.dll \
        Tao.Platform.Windows.dll
SYSTEM_LIBS = \
        System \
        System.Windows.Forms \
        System.Data \
        System.Drawing
RESX = \
        NeHe.resx
RESOURCES = \
        $(ASSEMBLY).NeHe.resources
SOURCES = \
        NeHe.cs \
        Lesson01.cs \
        Lesson02.cs \
        Lesson03.cs \
        Lesson04.cs \
        Lesson05.cs \
        Lesson06.cs \
        Lesson07.cs \
        Lesson08.cs \
        Lesson09.cs \
        Lesson10.cs \
        Lesson11.cs \
        Lesson12.cs \
        Lesson13.cs \
        Lesson14.cs \
        Lesson15.cs \
        Lesson16.cs \
        Lesson17.cs \
        Lesson18.cs \
        Lesson19.cs \
        Lesson20.cs \
        Lesson21.cs \
        Lesson23.cs \
        Lesson25.cs \
        Lesson26.cs \
        Lesson34.cs \
        NeHe.Designer.cs \
        Program.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) $(ASSEMBLY).csproj $(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

$(ASSEMBLY).NeHe.resources: NeHe.resx
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
	  /keyfile:$(srcdir)/../../src/Tao.FreeGlut/Tao.FreeGlut.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

