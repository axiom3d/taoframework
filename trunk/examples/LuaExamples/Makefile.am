
ASSEMBLY1 = LuaExamples.Functions
ASSEMBLY2 = LuaExamples.Simple
PACKAGES =
BINARY_LIBS = \
        Tao.Lua.dll
SYSTEM_LIBS = \
        System
RESX =
RESOURCES =
SOURCES1 = \
        Functions/Functions.cs \
        Functions/Properties/AssemblyInfo.cs
SOURCES2 = \
        Simple/Simple.cs \
        Simple/Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY1).exe $(ASSEMBLY2).exe

EXTRA_DIST = $(SOURCES1) $(SOURCES2) $(RESX) Functions/$(ASSEMBLY1).csproj Simple/$(ASSEMBLY2).csproj Functions/$(ASSEMBLY1).sh Simple/$(ASSEMBLY2).sh
CLEANFILES = $(ASSEMBLY1).exe $(ASSEMBLY2).exe $(RESOURCES)

$(ASSEMBLY1).exe: $(RESOURCES) $(SOURCES1)
	for lib in $(BINARY_LIBS); do \
	  ln -sf ../../src/$${lib%.dll}/$${lib}; \
	  ln -sf ../../src/$${lib%.dll}/$${lib}.config; \
	done
	$(CSC) /out:$@ /target:winexe \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /resource:, $(RESOURCES)) \
	  $(addprefix $(srcdir)/, $(SOURCES1)) \
	  /keyfile:$(srcdir)/../../src/Tao.FFmpeg/Tao.FFmpeg.snk

$(ASSEMBLY2).exe: $(RESOURCES) $(SOURCES2)
	for lib in $(BINARY_LIBS); do \
	  ln -sf ../../src/$${lib%.dll}/$${lib}; \
	  ln -sf ../../src/$${lib%.dll}/$${lib}.config; \
	done
	$(CSC) /out:$@ /target:winexe \
	  $(addprefix /r:$(srcdir)/, $(BINARY_LIBS)) \
	  $(addprefix /r:, $(SYSTEM_LIBS)) \
	  $(addprefix /resource:, $(RESOURCES)) \
	  $(addprefix $(srcdir)/, $(SOURCES2)) \
	  /keyfile:$(srcdir)/../../src/Tao.FFmpeg/Tao.FFmpeg.snk

clean-local:
	rm -f $(BINARY_LIBS)
	rm -f $(addsuffix .config, $(BINARY_LIBS))

