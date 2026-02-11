export class Engine {
    constructor(canvasId, width, height) {
        this.canvas = document.getElementById(canvasId);
        this.ctx = this.canvas.getContext('2d');
        this.width = width;
        this.height = height;

        this.canvas.width = this.width;
        this.canvas.height = this.height;
        this.canvas.style.width = '100%';
        this.canvas.style.height = '100%';

        this.lastTime = 0;
        this.accumulator = 0;
        this.deltaTime = 1 / 60; // Target 60 FPS

        this.entities = [];
        this.isRunning = false;
    }

    start() {
        this.isRunning = true;
        this.lastTime = performance.now();
        requestAnimationFrame(this.loop.bind(this));
    }

    loop(currentTime) {
        if (!this.isRunning) return;

        const frameTime = (currentTime - this.lastTime) / 1000;
        this.lastTime = currentTime;

        this.accumulator += frameTime;

        while (this.accumulator >= this.deltaTime) {
            this.update(this.deltaTime);
            this.accumulator -= this.deltaTime;
        }

        this.render();
        requestAnimationFrame(this.loop.bind(this));
    }

    update(dt) {
        for (const entity of this.entities) {
            if (entity.update) entity.update(dt);
        }
    }

    render() {
        this.ctx.clearRect(0, 0, this.width, this.height);

        // Render background/level first

        for (const entity of this.entities) {
            if (entity.render) entity.render(this.ctx);
        }
    }

    addEntity(entity) {
        this.entities.push(entity);
    }
}
