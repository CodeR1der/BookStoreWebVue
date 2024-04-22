<template>
        <Sidebar />
</template>

<script>
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
        }
    }

</script>