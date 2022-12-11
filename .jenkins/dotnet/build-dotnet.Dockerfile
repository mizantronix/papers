FROM ubuntu:22.10

RUN apt-get update && apt-get install -y dotnet-sdk-6.0 && \
  mkdir /home/agent && mkdir /home/agent/work

COPY ./build.sh /home/agent/

WORKDIR /home/agent/work

ENTRYPOINT ["bash", "/home/agent/build.sh"]