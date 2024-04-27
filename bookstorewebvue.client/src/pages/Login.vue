<template>
    <main class="login">
        <section class="forms">
            <form class="login-form" @submit.prevent="login">
                <h2>Вход</h2>
                <div class="mb-3">
                    <label for="email" class="form-label">Email</label>
                    <input type="email" id="email" class="form-control" v-model="email" />
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Пароль</label>
                    <input type="password" id="password" class="form-control" v-model="password" />
                </div>
                <button type="submit" class="btn btn-primary">Вход</button>
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
            };
        },
        methods: {
            login() {
                this.$store.dispatch('loginUser', { email: this.email, password: this.password })
                    .then(() => {
                        this.$router.push('/');
                    })
                    .catch(error => {
                        console.error(error);
                    });
            },
        },
    };
</script>

<style scoped>
    .login {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
    }

    .forms {
        max-width: 400px;
    }

    .login-form {
        padding: 20px;
        border: 1px solid #ddd;
        border-radius: 5px;
    }

        .login-form h2 {
            margin-bottom: 20px;
        }
</style>
