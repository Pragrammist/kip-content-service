all:
	docker build -t kip-content .
	docker pull mongo
	docker compose up