
ASSEMBLY = Tao.Ode.Tests
PACKAGES = \
        nunit
BINARY_LIBS = \
        ../../src/Tao.Ode/Tao.Ode.dll
SYSTEM_LIBS = \
        System
RESOURCES =
SOURCES = \
        Matrix3Marshalling.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

tao_ode_testsdir = $(prefix)/lib/cli/tao-ode-tests-@TAOODE_MAJOR@

noinst_DATA = $(ASSEMBLY).dll tao-ode-tests.pc

EXTRA_DIST = $(SOURCES) $(ASSEMBLY).csproj
CLEANFILES = $(ASSEMBLY).dll
DISTCLEANFILES = tao-ode-tests.pc

$(ASSEMBLY).dll: $(RESOURCES) $(SOURCES) $(BINARY_LIBS)
	$(CSC) /out:$@ /target:library /unsafe \
	  $(addprefix /pkg:, $(PACKAGES)) \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix $(srcdir)/, $(SOURCES))

install-data-local: $(ASSEMBLY).dll
	$(GACUTIL) /i $(ASSEMBLY).dll /f /package $(ASSEMBLY) $(GACUTIL_FLAGS)
	$(INSTALL) -d $(DESTDIR)$(pkgconfigdir)
	$(INSTALL) tao-ode-tests.pc $(DESTDIR)$(pkgconfigdir)/tao-ode-tests-@TAOODE_MAJOR@.pc
	ln -sf tao-ode-tests-@TAOODE_MAJOR@.pc $(DESTDIR)$(pkgconfigdir)/tao-ode-tests.pc

uninstall-local:
	$(GACUTIL) /u $(ASSEMBLY), Version=@TAOODE_VERSION@ \
	  /package $(ASSEMBLY) $(GACUTIL_FLAGS) || true
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-ode-tests-@TAOODE_MAJOR@.pc
	rm -f $(DESTDIR)$(pkgconfigdir)/tao-ode-tests.pc

