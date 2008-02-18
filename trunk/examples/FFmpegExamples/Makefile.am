
ASSEMBLY = FFmpegExamples
PACKAGES =
BINARY_LIBS = \
        Tao.FFmpeg.dll
SYSTEM_LIBS = \
        System \
        System.Windows.Forms \
        System.Data \
        System.Drawing
RESX = \
        FFmpegPlayer/Player.resx \
        FFmpegPlayer/Properties/Resources.resx
RESOURCES = \
        $(ASSEMBLY).Player.resources \
        $(ASSEMBLY).Resources.resources
SOURCES = \
        FFmpegPlayer/Player.cs \
        FFmpegPlayer/Properties/Resources.Designer.cs \
        FFmpegPlayer/AudioStream.cs \
        FFmpegPlayer/Decoder.cs \
        FFmpegPlayer/Player.Designer.cs \
        FFmpegPlayer/Program.cs \
        FFmpegPlayer/Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) FFmpegPlayer/$(ASSEMBLY).csproj FFmpegPlayer/$(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

$(ASSEMBLY).Player.resources: FFmpegPlayer/Player.resx
	$(RESGEN) $< $@
$(ASSEMBLY).Resources.resources: FFmpegPlayer/Properties/Resources.resx
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
	  /keyfile:$(srcdir)/../../src/Tao.FFmpeg/Tao.FFmpeg.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

