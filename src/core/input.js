export class Input {
    constructor() {
        this.keys = new Set();

        window.addEventListener('keydown', (e) => {
            this.keys.add(e.code);
        });

        window.addEventListener('keyup', (e) => {
            this.keys.delete(e.code);
        });
    }

    isPressed(key) {
        return this.keys.has(key);
    }

    get horizontal() {
        if (this.isPressed('ArrowLeft') || this.isPressed('KeyA')) return -1;
        if (this.isPressed('ArrowRight') || this.isPressed('KeyD')) return 1;
        return 0;
    }

    get jump() {
        return this.isPressed('ArrowUp') || this.isPressed('KeyW') || this.isPressed('Space');
    }

    get shoot() {
        return this.isPressed('ControlLeft') || this.isPressed('KeyK') || this.isPressed('KeyX');
    }
}
