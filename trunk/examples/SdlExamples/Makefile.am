
ASSEMBLY = SdlExamples
PACKAGES =
BINARY_LIBS = \
        Tao.Sdl.dll
SYSTEM_LIBS = \
        System \
        System.Windows.Forms \
        System.Data \
        System.Drawing
RESX = \
        SdlExamples.resx
RESOURCES = \
        $(ASSEMBLY).SdlExamples.resources
SOURCES = \
        SdlExamples.cs \
        GfxPrimitives.cs \
        Program.cs \
        Rectangles.cs \
        SdlExamples.Designer.cs \
        SmpegPlayer.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) $(ASSEMBLY).csproj $(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

$(ASSEMBLY).SdlExamples.resources: SdlExamples.resx
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
	  /keyfile:$(srcdir)/../../src/Tao.Sdl/Tao.Sdl.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

