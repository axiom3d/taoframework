#!/bin/sh
# Builds the Tao Framework using both Prebuild and NAnt 

rm -rf dist

# Build Solutions Using NAnt 
nant -t:mono-2.0 -buildfile:src/Tao.DevIl/Tao.DevIl.build clean
nant -t:mono-2.0 -buildfile:src/Tao.GlBindGen/Tao.GlBindGen.build clean 
nant -t:mono-2.0 -buildfile:src/Tao.Ode/Tao.Ode.build clean 
nant -t:mono-2.0 -buildfile:src/Tao.OpenGl/Tao.OpenGl.build clean 
nant -t:mono-2.0 -buildfile:src/Tao.PhysFs/Tao.PhysFs.build clean 
nant -t:mono-2.0 -buildfile:src/Tao.Sdl/Tao.Sdl.build clean 
nant -t:mono-2.0 -buildfile:src/Tao.Lua/Tao.Lua.build clean 
nant -t:mono-2.0 -buildfile:src/Tao.FFmpeg/Tao.FFmpeg.build clean 
nant -t:mono-2.0 -buildfile:src/Tao.FreeType/Tao.FreeType.build clean 
