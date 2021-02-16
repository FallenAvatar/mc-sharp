﻿using System.Drawing;

using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace mc_sharp {
	class Program {
		private static IWindow window;

		static void Main(string[] args) {
            //Create a window.
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "LearnOpenGL with Silk.NET";

            window = Window.Create(options);

            //Assign events.
            window.Load += OnLoad;
            window.Update += OnUpdate;
            window.Render += OnRender;

            //Run the window.
            window.Run();
        }

        private static void OnLoad() {
            //Set-up input context.
            IInputContext input = window.CreateInput();
            for( int i = 0; i < input.Keyboards.Count; i++ ) {
                input.Keyboards[i].KeyDown += KeyDown;
            }
        }

        private static void OnRender(double obj) {
            //Here all rendering should be done.
        }

        private static void OnUpdate(double obj) {
            //Here all updates to the program should be done.
        }

        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3) {
            //Check to close the window on escape.
            if( arg2 == Key.Escape ) {
                window.Close();
            }
        }
    }
}
