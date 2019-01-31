using System.Collections.Generic;
using GFDLibrary.Rendering.OpenGL;
using OpenTK.Graphics.OpenGL;
using Starfield.IO;

namespace Starfield.Graphics
{
    public class ShaderStore
    {
        private static ShaderStore sInstance;
        public static  ShaderStore Instance => sInstance ?? ( sInstance = new ShaderStore() );

        private readonly Dictionary<string, GLShaderProgram> mLoadedShaders;

        public ShaderStore()
        {
            mLoadedShaders = new Dictionary<string, GLShaderProgram>();
        }

        public GLShaderProgram Get( string name )
        {
            if ( !mLoadedShaders.TryGetValue( name, out var shader ) )
            {
                var vs = FileManager.Instance.OpenText( $"shaders/{name}.glsl.vs" ).ReadToEnd();
                var fs = FileManager.Instance.OpenText( $"shaders/{name}.glsl.fs" ).ReadToEnd();

                using ( var builder = new GLShaderProgramBuilder() )
                {
                    
                    if ( !builder.TryAttachShader( ShaderType.VertexShader, vs ) )
                        return null;

                    if ( !builder.TryAttachShader( ShaderType.FragmentShader, fs ) )
                        return null;

                    if ( !builder.TryBuild( out var id ) )
                        return null;

                    shader = new GLShaderProgram( id );
                }

                mLoadedShaders[name] = shader;
            }

            return shader;
        }
    }
}