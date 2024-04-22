<template>
    <main class="register">
        <section class="forms">
            <form class="register-form" @submit.prevent="register">
                <h2>Регистрация</h2>
                <div class="mb-3">
                    <label for="nickname" class="form-label">Никнейм</label>
                    <input type="text" id="nickname" class="form-control" v-model="nickname" />
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email</label>
                    <input type="email" id="email" class="form-control" v-model="email" />
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Пароль</label>
                    <input type="password" id="password" class="form-control" v-model="password" />
                </div>
                <div class="mb-3">
                    <label for="confirmPassword" class="form-label">Подтвердите пароль</label>
                    <input type="password" id="confirmPassword" class="form-control" v-model="confirmPassword" />
                </div>
                <button type="submit" class="btn btn-primary">Регистрация</button>
            </form>
        </section>
    </main>
</template>

<script>
    export default {
        data() {
            return {
                email: '',
                password: '',
                nickname: '',
            };
        },
        methods: {
            register() {
                if (this.password !== this.confirmPassword) {
                    console.error('Passwords do not match');
                    // Handle error
                    return;
                }
                const user = {
                    email: this.email,
                    password: this.password,
                    isAdmin: false,
                    nickname: this.nickname,
                };
                this.$store.dispatch('registerUser', user)
                    .then(() => {
                        this.$router.push('/books');
                    })
                    .catch(error => {
                        console.error(error);
                        // Handle error
                    });
            },
        },
    };
</script>

<style scoped>
    .register {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
    }

    .forms {
        max-width: 400px;
    }

    .register-form {
        padding: 20px;
        border: 1px solid #ddd;
        border-radius: 5px;
    }

        .register-form h2 {
            margin-bottom: 20px;
        }
</style>
