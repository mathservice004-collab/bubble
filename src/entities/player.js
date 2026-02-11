export class Player {
    constructor(x, y, color = '#5cd6ff') {
        this.x = x;
        this.y = y;
        this.width = 16;
        this.height = 16;
        this.color = color;

        this.vx = 0;
        this.vy = 0;
        this.speed = 100;
        this.jumpPower = -250;
        this.gravity = 600;

        this.isGrounded = false;
        this.facing = 1; // 1 for right, -1 for left
    }

    update(dt, input) {
        // Horizontal Movement
        const move = input.horizontal;
        this.vx = move * this.speed;
        if (move !== 0) this.facing = move;

        // Gravity
        this.vy += this.gravity * dt;

        // Jump
        if (input.jump && this.isGrounded) {
            this.vy = this.jumpPower;
            this.isGrounded = false;
        }

        // Apply Velocity
        this.x += this.vx * dt;
        this.y += this.vy * dt;

        // Simple Floor Collision (placeholder for real level logic)
        if (this.y > 200) {
            this.y = 200;
            this.vy = 0;
            this.isGrounded = true;
        }

        // Screen Wrap (Vertical only for BB)
        if (this.y > 224) this.y = -16;
        if (this.y < -16) this.y = 224;

        // Screen Clamp (Horizontal)
        if (this.x < 0) this.x = 0;
        if (this.x > 256 - this.width) this.x = 256 - this.width;
    }

    render(ctx) {
        ctx.fillStyle = this.color;
        ctx.fillRect(this.x, this.y, this.width, this.height);

        // Eyes to show direction
        ctx.fillStyle = '#fff';
        const eyeX = this.facing === 1 ? this.x + 10 : this.x + 2;
        ctx.fillRect(eyeX, this.y + 4, 4, 4);
    }
}
