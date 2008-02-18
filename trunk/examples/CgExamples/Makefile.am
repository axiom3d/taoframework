
ASSEMBLY = CgExamples
PACKAGES =
BINARY_LIBS = \
        Tao.Cg.dll \
        Tao.FreeGlut.dll \
        Tao.OpenGl.dll
SYSTEM_LIBS = \
        System \
        System.Windows.Forms \
        System.Data \
        System.Drawing
RESX = \
        CgExamples.resx
RESOURCES = \
        $(ASSEMBLY).CgExamples.resources
SOURCES = \
        CgExamples.cs \
        CgExamples.Designer.cs \
        Gl_01_vertex_program.cs \
        Gl_02_vertex_and_fragment_program.cs \
        Gl_06_vertex_twisting_program.cs \
        Gl_14_bulge_program.cs \
        Program.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) $(ASSEMBLY).csproj $(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

$(ASSEMBLY).CgExamples.resources: CgExamples.resx
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
	  /keyfile:$(srcdir)/../../src/Tao.Cg/Tao.Cg.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

