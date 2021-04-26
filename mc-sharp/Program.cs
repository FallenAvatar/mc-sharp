
using System;

using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace McSharp {
	class Program {
		private static IWindow window;
		private static GL Gl;

		//Our new abstracted objects, here we specify what the types are.
		private static BufferObject<float> Vbo;
		private static BufferObject<uint> Ebo;
		private static VertexArrayObject<float, uint> Vao;
		//Create a texture object.
		private static Texture Texture;
		private static Shader Shader;

		private static readonly float[] Vertices = {
            //X    Y      Z     U   V
             0.5f,  0.5f, 0.0f, 1f, 1f,
			 0.5f, -0.5f, 0.0f, 1f, 0f,
			-0.5f, -0.5f, 0.0f, 0f, 0f,
			-0.5f,  0.5f, 0.5f, 0f, 1f
		};

		private static readonly uint[] Indices = {
			0, 1, 3,
			1, 2, 3
		};


		private static void Main( string[] args ) {
			var options = WindowOptions.Default;
			options.Size = new Vector2D<int>( 600, 600 );
			options.Title = "LearnOpenGL with Silk.NET";
			window = Window.Create( options );

			window.Load += OnLoad;
			window.Render += OnRender;
			window.Closing += OnClose;

			window.Run();
		}


		private static void OnLoad() {
			IInputContext input = window.CreateInput();
			for( int i = 0; i < input.Keyboards.Count; i++ ) {
				input.Keyboards[i].KeyDown += KeyDown;
			}

			Gl = GL.GetApi( window );

			//Instantiating our new abstractions
			Ebo = new BufferObject<uint>( Gl, Indices, BufferTargetARB.ElementArrayBuffer );
			Vbo = new BufferObject<float>( Gl, Vertices, BufferTargetARB.ArrayBuffer );
			Vao = new VertexArrayObject<float, uint>( Gl, Vbo, Ebo );

			//Telling the VAO object how to lay out the attribute pointers
			Vao.VertexAttributePointer( 0, 3, VertexAttribPointerType.Float, 5, 0 );
			Vao.VertexAttributePointer( 1, 2, VertexAttribPointerType.Float, 5, 3 );

			Shader = new Shader( Gl, "gl/shaders/shader.vert", "gl/shaders/shader.frag" );

			//Loading a texture.
			Texture = new Texture( Gl, "tex/silk.png" );
		}

		private static unsafe void OnRender( double obj ) {
			Gl.Clear( (uint)ClearBufferMask.ColorBufferBit );

			//Binding and using our VAO and shader.
			Vao.Bind();
			Shader.Use();
			//Bind a texture and and set the uTexture0 to use texture0.
			Texture.Bind( TextureUnit.Texture0 );
			Shader.SetUniform( "uTexture0", 0 );

			Gl.DrawElements( PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null );
		}

		private static void OnClose() {
			//Remember to dispose all the instances.
			Vbo.Dispose();
			Ebo.Dispose();
			Vao.Dispose();
			Shader.Dispose();
			//Remember to dispose the texture.
			Texture.Dispose();
		}

		private static void KeyDown( IKeyboard arg1, Key k, int arg3 ) {
			if( k == Key.Escape ) {
				window.Close();
			}
		}
	}
}
