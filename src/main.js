import { Engine } from './core/engine.js';
import { Input } from './core/input.js';
import { Player } from './entities/player.js';

const GAME_WIDTH = 256;
const GAME_HEIGHT = 224;

const engine = new Engine('gameCanvas', GAME_WIDTH, GAME_HEIGHT);
const input = new Input();
const player = new Player(32, 200);

function init() {
    console.log("Bubble Bobble: Reborn Classic Initialized");

    engine.addEntity({
        update(dt) {
            player.update(dt, input);
        },
        render(ctx) {
            player.render(ctx);
        }
    });

    engine.start();

    // Hide overlay on interaction
    document.addEventListener('keydown', () => {
        document.getElementById('overlay').classList.add('hidden');
    }, { once: true });
}

window.addEventListener('load', init);
