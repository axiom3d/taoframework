
ASSEMBLY = Redbook
PACKAGES =
BINARY_LIBS = \
        Tao.FreeGlut.dll \
        Tao.OpenGl.dll
SYSTEM_LIBS = \
        System \
        System.Windows.Forms \
        System.Data \
        System.Drawing
RESX = \
        Redbook.resx
RESOURCES = \
        $(ASSEMBLY).Redbook.resources
SOURCES = \
        Redbook.cs \
        Aaindex.cs \
        Aapoly.cs \
        Aargb.cs \
        Accanti.cs \
        Accpersp.cs \
        Alpha.cs \
        Alpha3d.cs \
        Anti.cs \
        Bezcurve.cs \
        Bezmesh.cs \
        Bezsurf.cs \
        Checker.cs \
        CheckerOld.cs \
        Clip.cs \
        Colormat.cs \
        Cube.cs \
        DepthCue.cs \
        Dof.cs \
        Double.cs \
        Drawf.cs \
        Feedback.cs \
        Fog.cs \
        FogIndex.cs \
        FogIndexOld.cs \
        FogOld.cs \
        Font.cs \
        Hello.cs \
        Image.cs \
        Jitter.cs \
        Light.cs \
        Lines.cs \
        List.cs \
        Material.cs \
        Mipmap.cs \
        Model.cs \
        MoveLight.cs \
        Nurbs.cs \
        PickDepth.cs \
        PickSquare.cs \
        Plane.cs \
        Planet.cs \
        PolyOff.cs \
        Polys.cs \
        Program.cs \
        Quadric.cs \
        Redbook.Designer.cs \
        Robot.cs \
        Scene.cs \
        SceneBlueAmbient.cs \
        SceneColorLight.cs \
        SceneFlat.cs \
        Select.cs \
        Smooth.cs \
        Stencil.cs \
        Stroke.cs \
        Surface.cs \
        SurfaceOld.cs \
        TeaAmbient.cs \
        Teapots.cs \
        Tess.cs \
        TessWind.cs \
        TexBind.cs \
        TexGen.cs \
        TexProx.cs \
        TexSub.cs \
        TextureSurf.cs \
        Torus.cs \
        Trim.cs \
        UnProject.cs \
        Varray.cs \
        Wrap.cs \
        Properties/AssemblyInfo.cs

pkgconfigdir = $(prefix)/lib/pkgconfig

noinst_DATA = $(ASSEMBLY).exe

EXTRA_DIST = $(SOURCES) $(RESX) $(ASSEMBLY).csproj $(ASSEMBLY).sh
CLEANFILES = $(ASSEMBLY).exe $(RESOURCES)

$(ASSEMBLY).Redbook.resources: Redbook.resx
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

