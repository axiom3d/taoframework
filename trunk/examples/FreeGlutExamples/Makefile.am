
ASSEMBLY = FreeGlutExamples.One
PACKAGES =
BINARY_LIBS = \
        Tao.FreeGlut.dll \
        Tao.OpenGl.dll
SYSTEM_LIBS = \
        System
RESX =
RESOURCES =
SOURCES = \
        One/One.cs \
        One/Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) One/$(ASSEMBLY).csproj One/$(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

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
	  /keyfile:$(srcdir)/../../src/Tao.FFmpeg/Tao.FFmpeg.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

