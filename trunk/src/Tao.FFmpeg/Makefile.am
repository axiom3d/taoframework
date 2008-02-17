
ASSEMBLY = Tao.FFmpeg
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        AVCodec.cs \
        AVFormat.cs \
        AVSWScale.cs \
        AVUtil.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_ffmpegdir = $(prefix)/lib/cli/tao-ffmpeg-@TAOFFMPEG_MAJOR@
tao_ffmpeg_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-ffmpeg.pc

EXTRA_DIST = $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-ffmpeg.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-ffmpeg.pc $(DESTDIR)$(pkgconfigdir)/tao-ffmpeg-@TAOFFMPEG_MAJOR@.pc
	ln -sf tao-ffmpeg-@TAOFFMPEG_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-ffmpeg.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOFFMPEG_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-ffmpeg-@TAOFFMPEG_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-ffmpeg.pc

