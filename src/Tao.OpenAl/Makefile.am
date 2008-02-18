
ASSEMBLY = Tao.OpenAl
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Al.cs \
	Alc.cs \
	Alut.cs \
	ALDelegates.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_openaldir = $(prefix)/lib/cli/tao-openal-@TAOOPENAL_MAJOR@
tao_openal_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-openal.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj \
             $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll $(ASSEMBLY).xml
DISTCLEANFILES = tao-openal.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-openal.pc $(DESTDIR)$(pkgconfigdir)/tao-openal-@TAOOPENAL_MAJOR@.pc
	ln -sf tao-openal-@TAOOPENAL_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-openal.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOOPENAL_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-openal-@TAOOPENAL_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-openal.pc

