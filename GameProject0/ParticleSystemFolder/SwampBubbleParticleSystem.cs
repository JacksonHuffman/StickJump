using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject0.ParticleSystemFolder
{
    public class SwampBubbleParticleSystem : ParticleSystem
    {
        Rectangle _source;

        public bool IsBubbling { get; set; } = true;

        public SwampBubbleParticleSystem(Game game, Rectangle source) : base(game, 4000)
        {
            _source = source;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "Bubble";
            minNumParticles = 5;
            maxNumParticles = 10;

            blendState = BlendState.Additive;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, Vector2.UnitY * -100, Vector2.Zero, Color.White, scale: RandomHelper.NextFloat(0.3f, 0.6f), lifetime: 0.2f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //AddParticles(_source);

            if(IsBubbling) AddParticles(_source);
        }
    }
}
