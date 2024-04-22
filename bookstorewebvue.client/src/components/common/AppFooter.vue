<template>
    <div class="footer">
        <div class="content">
            <p>&copy; 2024 Литературный ритм. Все права защищены.</p>
        </div>
    </div>
</template>

<script>
    export default {
        name: "Footer"
    }
</script>

<style scoped>
    .content {
        margin: 0px auto;
        background-color: #304156;
        width: 100%;
        height: 40px;
        line-height: 40px;
        color: white;
        text-align: center;
    }

    .footer {
        width: 100%;
        background-color: #304156;
        height: 40px;
        clear: both;
        margin-top: auto;
        z-index: 999;
    }
</style>