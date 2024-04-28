<template>
    <div>
        <Sidebar />
        <div class="main">
            <h2>Личная информация</h2>
            <form>
                <div class="mb-3">
                    <label for="firstname" class="form-label">Имя:</label>
                    <input type="text" id="firstname" v-model="currentUser.unique_name" class="form-control">
                </div>
                <div class="mb-3">
                    <label for="lastname" class="form-label">Фамилия:</label>
                    <input type="text" id="lastname" v-model="currentUser.family_name" class="form-control">
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email:</label>
                    <input type="text" id="email" v-model="currentUser.email" class="form-control" disabled>
                </div>
                <button @click.prevent="updateCustomer" class="btn btn-primary">Сохранить</button>
            </form>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import { computed } from 'vue';
    import { useStore } from 'vuex';
    import Sidebar from '@/pages/profile/Profile-sidebar.vue';

    export default {
        components: {
            Sidebar
        },
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);

            return {
                currentUser
            };
        },
        methods: {
            async updateCustomer() {
                const patchOperations = [
                    { op: 'replace', path: '/firstName', value: this.currentUser.unique_name },
                    { op: 'replace', path: '/lastName', value: this.currentUser.family_name }
                ]; 
                try {
                    const response = await axios.patch(`users/${this.currentUser.userId}/patch`, patchOperations);
                    const newToken = response.data.token;
                    localStorage.removeItem('token'); 
                    localStorage.setItem('token', newToken);
                } catch (error) {
                    console.error(error);
                }
            }
        },

    };
</script>

<style scoped>
    .main {
        margin-left: 200px; /* Ширина боковой панели */
        padding: 20px; /* Добавляем некоторый внутренний отступ для основного контента */
    }
</style>
