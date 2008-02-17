
ASSEMBLY = Tao.Lua
PACKAGES =
BINARY_LIBS =
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Lua.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_luadir = $(prefix)/lib/cli/tao-lua-@TAOLUA_MAJOR@
tao_lua_DATA = $(ASSEMBLY).dll.config

noinst_DATA = $(ASSEMBLY).dll tao-lua.pc

EXTRA_DIST = $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).dll.config $(ASSEMBLY).snk
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-lua.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS) $(ASSEMBLY).snk
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES)) \
	  /doc:$(ASSEMBLY).xml /keyfile:$(srcdir)/$(ASSEMBLY).snk

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-lua.pc $(DESTDIR)$(pkgconfigdir)/tao-lua-@TAOLUA_MAJOR@.pc
	ln -sf tao-lua-@TAOLUA_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-lua.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOLUA_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-lua-@TAOLUA_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-lua.pc

